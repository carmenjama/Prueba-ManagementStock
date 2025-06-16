import React, { useState, useEffect } from 'react';
import Sidebar from '../../partials/Sidebar';
import Header from '../../partials/Header';
import SearchForm from '../../partials/actions/SearchForm';
import InvoicesTable from '../../partials/invoices/InvoicesTable';
import ModalBasic from "../../components/ModalBasic";
import { getUrl } from '../../utils/Auth';
import { getToken } from '../../utils/Auth';

function Invoices() {
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [basicModalOpen, setBasicModalOpen] = useState(false);
  const [feedbackModalOpen, setFeedbackModalOpen] = useState(false);
  const [files, setFiles] = useState([]);
  const [categories, setCategory] = useState([]);
  const [refreshInvoices, setRefreshInvoices] = useState(false);
  const [data, setData] = useState({
    categoryId: 0,
    code : "",
    name : "",
    price : 0,
    unit : "",
    note : "",
    multimedia : ""
  });

  useEffect(() => {
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
  }, []); 
  

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

  const handleSaveProduct = () => {
    const base64File = files[0]
      ? fileToBase64(files[0]).then(base64 => {
        try {
          const response = fetch(getUrl('product'), {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
              'Authorization': getToken()
            },
            body: JSON.stringify({...data, multimedia: base64})
          });
          if (!response.ok) {
            throw new Error('Error al cargar las facturas');
          }
          setRefreshInvoices(prev => !prev);
          setFeedbackModalOpen(false);
        } catch (err) {
          
        }
       })
      : ()=>{
       save(null) 
      } ;
      setRefreshInvoices(prev => !prev);
      setFeedbackModalOpen(false);
  };

  const save = () => {
    const response = fetch(getUrl('product'), {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
              'Authorization': getToken()
            },
            body: JSON.stringify({...data, multimedia: null})
          });
          if (!response.ok) {
            throw new Error('Error al cargar las facturas');
          }
          setRefreshInvoices(prev => !prev);
          setFeedbackModalOpen(false);
  }

  const fileToBase64 = (file) => {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result); // Base64 result
      reader.onerror = (error) => reject(error);
    });
  };

  const handleFileChange = (e) => {
    const selectedFiles = Array.from(e.target.files);
    setFiles(selectedFiles);
  };

  const handleRemoveFile = (index) => {
    const newFiles = files.filter((_, i) => i !== index);
    setFiles(newFiles);
  };

  const handleCloseModal = () => {
    setModalOpen(false);
  };

  return (
    <div className="flex h-[100dvh] overflow-hidden">

      {/* Sidebar */}
      <Sidebar sidebarOpen={sidebarOpen} setSidebarOpen={setSidebarOpen} />

      {/* Content area */}
      <div className="relative flex flex-col flex-1 overflow-y-auto overflow-x-hidden">

        {/*  Site header */}
        <Header sidebarOpen={sidebarOpen} setSidebarOpen={setSidebarOpen} />

        <main className="grow">
          <div className="px-4 sm:px-6 lg:px-8 py-8 w-full max-w-[96rem] mx-auto">

            {/* Page header */}
            <div className="sm:flex sm:justify-between sm:items-center mb-5">

              {/* Left: Title */}
              <div className="mb-4 sm:mb-0">
                <h1 className="text-2xl md:text-3xl text-gray-800 dark:text-gray-100 font-bold">Productos</h1>
              </div>

              {/* Right: Actions */}
              <div className="grid grid-flow-col sm:auto-cols-max justify-start sm:justify-end gap-2">

                {/* Search form */}
                <SearchForm placeholder="Buscar..." />
                
                {/* Start */}
                <button
                  className="btn bg-gray-900 text-gray-100 hover:bg-gray-800 dark:bg-gray-100 dark:text-gray-800 dark:hover:bg-white"
                  aria-controls="feedback-modal"
                  onClick={(e) => {
                    e.stopPropagation();
                    setFeedbackModalOpen(true);
                  }}>Agregar
                </button>

                {/* Modal add product */}
                <ModalBasic id="feedback-modal" modalOpen={feedbackModalOpen} 
                  setModalOpen={setFeedbackModalOpen} title="Agregar producto">
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
                          setFeedbackModalOpen(false);
                        }}>
                          Cancelar
                      </button>
                      <button onClick={() => handleSaveProduct()} className="btn-sm bg-gray-900 text-gray-100 hover:bg-gray-800 dark:bg-gray-100 dark:text-gray-800 dark:hover:bg-white">Enviar</button>
                    </div>
                  </div>
                </ModalBasic>
              </div>
            </div>

            {/* Table */}
            <InvoicesTable refresh={refreshInvoices} setRefresh={setRefreshInvoices}/>

          </div>
        </main>
      </div>
    </div>
  );
}

export default Invoices;