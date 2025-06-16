import React, { useState, useEffect } from 'react';
import Invoices from './InvoicesTableItem';
import PaginationClassic from '../../components/PaginationClassic';
import { getToken } from '../../utils/Auth';
import { getUrl } from '../../utils/Auth';
import ModalBasic from "../../components/ModalBasic";
import SaleModal from "../../partials/invoices/SaleModal";

function InvoicesTable({ refresh }) {
  const [loading, setLoading] = useState(true);  // Para manejar el estado de carga
  const [error, setError] = useState(null);      // Para manejar errores
  const [isCheck, setIsCheck] = useState([]);
  const [list, setList] = useState([]);
  const [editModalOpen, setEditModalOpen] = useState(false);
  const [productToEdit, setProductToEdit] = useState(null);
  const [saleModalOpen, setSaleModalOpen] = useState(false);
  const [selectedProductId, setSelectedProductId] = useState(null);
  const [page, SetPage] = useState({
    currentPage : 1,
    limit: 2
  });
  const [data, setData] = useState({
    categoryId: "",
    code: "",
    name: "",
    price: "",
    unit: "",
    note: ""
  });
  const [files, setFiles] = useState([]);
  const [categories, setCategories] = useState([]);

  const handleChange = (e) => {
      const { id, value } = e.target;
      switch (id) {
        case 'category':
          setData({
            ...data,
            categoryId: value
          });
          break;
        case 'code':
          setData({
            ...data,
            code: value
          });
          break;
        case 'name':
          setData({
            ...data,
            name: value
          });
          break;
        case 'price':
          setData({
            ...data,
            price: value
          });
          break;
        case 'unit':
          setData({
            ...data,
            unit: value
          });
          break;
        case 'note':
          setData({
            ...data,
            note: value
          });
          break;
        default:
          break;
      }
    };

    useEffect(() => {
      if (productToEdit) {
        setData({
          categoryId: productToEdit.categoryId || "",
          code: productToEdit.code || "",
          name: productToEdit.name || "",
          price: productToEdit.price || "",
          unit: productToEdit.unit || "",
          note: productToEdit.note || ""
        });
      }
    }, [productToEdit]);
    
    useEffect(() => {
      const fetchInvoices = async () => {
        try {
          const response = await fetch(getUrl('product/true?page='+page.currentPage+'&limit='+page.limit), {
            method: 'GET',
            headers: {
              'Content-Type': 'application/json',
              'Authorization': getToken()
            }
          });
          if (!response.ok) {
            throw new Error('Error al cargar las facturas');
          }
          const data = await response.json();
          setList(data.elements); 
          SetPage(prevPage => ({
            ...prevPage,
            currentPage: data.currentPage,
            numberPages: data.numberPages,
            totalRecords: data.totalRecords,
            totalRecordsPage: data.totalRecordsPage
          }));
          setLoading(false); 
        } catch (err) {
          setError(err.message); 
          setLoading(false);
        }

      const fetCategory = async () => {
            try {
              const response = await fetch(getUrl('category/false'), {
                method: 'GET',
                headers: {
                  'Content-Type': 'application/json',
                  'Authorization': getToken()
                }
              });
              if (!response.ok) {
                throw new Error('Error al cargar las categorías');
              }
              const data = await response.json();
              setCategory([...data]);
            } catch (err) {
              
            }
          };
      
          fetCategory();
    };

  fetchInvoices();
    if (productToEdit) {
      setData({
        categoryId: productToEdit.categoryid || "",
        code: productToEdit.code || "",
        name: productToEdit.name || "",
        price: productToEdit.price || "",
        unit: productToEdit.unit || "",
        note: productToEdit.note || ""
      });
    }
  }, [page.currentPage, refresh, productToEdit]);

  const handleFileChange = (e) => {
    const selectedFiles = Array.from(e.target.files);
    setFiles(selectedFiles);
  };

  const handleRemoveFile = (index) => {
    const newFiles = files.filter((_, i) => i !== index);
    setFiles(newFiles);
  };
  
  const handleSaveProduct = async () => {
    try {
      const formData = new FormData();
      Object.entries(data).forEach(([key, value]) => {
        formData.append(key, value);
      });
      files.forEach((file) => {
        formData.append("files", file);
      });

      const response = await fetch(getUrl(`product/${productToEdit.id}`), {
        method: 'PUT',
        headers: {
          Authorization: getToken()
        },
        body: formData
      });

      if (!response.ok) throw new Error('Error al actualizar el producto');

      setEditModalOpen(false);
      setProductToEdit(null);
      refresh(); // recargar la tabla

    } catch (err) {
      console.error(err);
      alert("Error al actualizar el producto");
    }
  };

  const handleEditClick = (product) => {
    setProductToEdit(product);
    setEditModalOpen(true);
  };

  const handleDeleteClick = id => {
    try {
        const response = fetch(getUrl("product/"+id), {
          method: 'DELETE',
          headers: {
            'Content-Type': 'application/json',
            'Authorization':  getToken()
          },
        });
        if (!response.ok) {
          
        }
      } catch (err) {
        setError(err.message); 
      }
  };

  const handleSaleClick = id => {
    try {
        setSelectedProductId(id);
        setSaleModalOpen(true);
        console.log("id", id, saleModalOpen)
      } catch (err) {
        setError(err.message); 
      }
  };

  const handleSaleSuccess = () => {
    // setSaleModalOpen(false);
    // setSelectedProductId(null);
    refresh();
  };

  const handleBuyClick = id => {
    try {
        // const response = fetch(getUrl("product/"+id), {
        //   method: 'DELETE',
        //   headers: {
        //     'Content-Type': 'application/json',
        //     'Authorization':  getToken()
        //   },
        // });
        // if (!response.ok) {
          
        // }
      } catch (err) {
        setError(err.message); 
      }
  };

  const handlePageChange = (direction) => {
    SetPage(prevPage => {
      let newPage = prevPage.currentPage;

      if (direction === 'next' && newPage < prevPage.numberPages) {
        newPage += 1;
      } else if (direction === 'prev' && newPage > 1) {
        newPage -= 1;
      }

      return {
        ...prevPage,
        currentPage: newPage
      };
    });
  };

  return (
    <div className="bg-white dark:bg-gray-800 shadow-xs rounded-xl relative">
      <div>
        {/* Table */}
        <div className="overflow-x-auto">
          <table className="table-auto w-full dark:text-gray-300">
            {/* Table header */}
            <thead className="text-xs font-semibold uppercase text-gray-500 dark:text-gray-400 bg-gray-50 dark:bg-gray-900/20 border-t border-b border-gray-100 dark:border-gray-700/60">
              <tr>
                <th className="w-30 px-2 first:pl-5 last:pr-5 py-3 whitespace-nowrap">
                  <div className="font-semibold text-left">Código</div>
                </th>
                <th className="w-100 px-2 first:pl-5 last:pr-5 py-3 whitespace-nowrap">
                  <div className="font-semibold text-left">Nombre</div>
                </th>
                <th className="w-50 px-2 first:pl-5 last:pr-5 py-3 whitespace-nowrap">
                  <div className="font-semibold text-left">Categoría</div>
                </th>
                <th className="w-50 px-2 first:pl-5 last:pr-5 py-3 whitespace-nowrap">
                  <div className="font-semibold text-left">Precio</div>
                </th>
                <th className="w-50 px-2 first:pl-5 last:pr-5 py-3 whitespace-nowrap">
                  <div className="font-semibold text-left">Stock</div>
                </th>
                <th className="w-50 px-2 first:pl-5 last:pr-5 py-3 whitespace-nowrap">
                  <div className="font-semibold text-left">Unidad</div>
                </th>
                <th className="w-50 px-2 first:pl-5 last:pr-5 py-3 whitespace-nowrap">
                  <div className="font-semibold text-left">Estado</div>
                </th>
                <th className="w-100 px-2 first:pl-5 last:pr-5 py-3 whitespace-nowrap">
                  <div className="font-semibold text-left">Notas</div>
                </th>
                <th className="px-2 first:pl-5 last:pr-5 py-3 whitespace-nowrap">
                  <div className="font-semibold text-left">Opciones</div>
                </th>
              </tr>
            </thead>
            {/* Table body */}
            <tbody className="text-sm divide-y divide-gray-100 dark:divide-gray-700/60">
              {
                list.map(product => {
                  return (
                    <Invoices
                      key={product.id}
                      id={product.id}
                      categoryid={product.categoryId}
                      code={product.code}
                      name={product.name}
                      categoryname={product.categoryName}
                      price={product.price}
                      stock={product.stock}
                      unit={product.unit}
                      note={product.note}
                      hasmultimedia={product.hasMultimedia}
                      status={product.status}
                      handleDeleteClick={handleDeleteClick}
                      handleSaleClick={handleSaleClick}
                      handleBuyClick={handleBuyClick}
                      handleEditClick={() => handleEditClick(product)}
                    />
                  )
                })
              }
            </tbody>
          </table>
        </div>  
      </div>
      
      {/* Pagination */}
      <div className="mt-8">
        <PaginationClassic page={page} handlePageChange={handlePageChange} />
      </div>
      <div>
        <SaleModal
                modalOpen={saleModalOpen}
                setModalOpen={setSaleModalOpen}
                productId={selectedProductId}
                onSaleSuccess={handleSaleSuccess}
              />
      </div>
      <div>
        <ModalBasic id="edit-product" modalOpen={editModalOpen} setModalOpen={setEditModalOpen} title="Editar Producto">
                <div className="px-5 py-4">
                    <div className="space-y-3">
                      {/* Categoría*/}
                      <div>
                        <label className="block text-sm font-medium mb-1" htmlFor="category">Categoría <span className="text-red-500">*</span></label>
                        <select
                          id="category"
                          name="category"
                          className="form-select w-full px-2 py-1"
                          value={data.categoryId || 0}
                          onChange={handleChange}
                          required
                        >
                          <option key="0" value="">Seleccione categoría</option>
                          {categories.map((cat) => (
                            <option key={cat.id} value={cat.id}>{cat.name}</option>
                          ))}
                        </select>
                      </div>
                      <div>
                        <label className="block text-sm font-medium mb-1" htmlFor="code">Código <span className="text-red-500">*</span></label>
                        <input id="code" className="form-input w-full px-2 py-1" type="text" value={data.code} onChange={handleChange} required />
                      </div>
                      <div>
                        <label className="block text-sm font-medium mb-1" htmlFor="name">Nombre <span className="text-red-500">*</span></label>
                        <input id="name" className="form-input w-full px-2 py-1" type="text" value={data.name} onChange={handleChange} required />
                      </div>
                      <div>
                        <label className="block text-sm font-medium mb-1" htmlFor="price">Precio <span className="text-red-500">*</span></label>
                        <input id="price" className="form-input w-full px-2 py-1" type="number" value={data.price} onChange={handleChange} required />
                      </div>
                      <div>
                        <label className="block text-sm font-medium mb-1" htmlFor="unit">Unidad <span className="text-red-500">*</span></label>
                        <input id="unit" className="form-input w-full px-2 py-1" type="text" value={data.unit} onChange={handleChange} required />
                      </div>
                      <div>
                        <label className="block text-sm font-medium mb-1" htmlFor="note">Notas<span className="text-red-500"></span></label>
                        <textarea id="note" className="form-textarea w-full px-2 py-1" rows="4" value={data.note} onChange={handleChange} required></textarea>
                      </div>

                      {/* Carga de archivos */}
                      <div>
                        <label className="block text-sm font-medium mb-1" htmlFor="file-upload">Archivos</label>
                        <input
                          id="file-upload"
                          className="form-input w-full px-2 py-1"
                          type="file"
                          multiple
                          onChange={handleFileChange}
                        />
                        <div className="mt-2">
                          {files.length > 0 && (
                            <div>
                              <h4 className="text-sm font-medium">Archivos seleccionados:</h4>
                              <ul className="space-y-2">
                                {files.map((file, index) => (
                                  <li key={index} className="flex items-center justify-between">
                                    <span className="text-sm text-gray-600">{file.name}</span>
                                    <button
                                      className="text-sm text-red-500"
                                      onClick={() => handleRemoveFile(index)}
                                    >
                                      Eliminar
                                    </button>
                                  </li>
                                ))}
                              </ul>
                            </div>
                          )}
                        </div>
                      </div>
                    </div>
                  </div>

                  {/* Modal footer */}
                  <div className="px-5 py-4 border-t border-gray-200 dark:border-gray-700/60">
                    <div className="flex flex-wrap justify-end space-x-2">
                      <button
                        className="btn-sm border-gray-200 dark:border-gray-700/60 hover:border-gray-300 dark:hover:border-gray-600 text-gray-800 dark:text-gray-300"
                        onClick={(e) => {
                          e.stopPropagation();
                          setEditModalOpen(false);
                        }}>
                          Cancelar
                      </button>
                      <button onClick={() => handleSaveProduct()} className="btn-sm bg-gray-900 text-gray-100 hover:bg-gray-800 dark:bg-gray-100 dark:text-gray-800 dark:hover:bg-white">Enviar</button>
                    </div>
                  </div>
              </ModalBasic>
      </div>
    </div>
  );
}

export default InvoicesTable;
