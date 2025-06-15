import React, { useState } from 'react';

import Sidebar from '../../partials/Sidebar';
import Header from '../../partials/Header';
import SearchForm from '../../partials/actions/SearchForm';
import InvoicesTable from '../../partials/invoices/InvoicesTable';
import ModalBasic from "../../components/ModalBasic";

function Invoices() {

  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [selectedItems, setSelectedItems] = useState([]);
  const [basicModalOpen, setBasicModalOpen] = useState(false);
  const [feedbackModalOpen, setFeedbackModalOpen] = useState(false);
  const [files, setFiles] = useState([]);
  const [data, setData] = useState({
    categoryId: 0,
    code : "",
    name : "",
    price : 0,
    unit : "",
    note : "",
    multimedia : ""
  });

  const handleChange = (e) => {
    const { id, value } = e.target;
    switch (id) {
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
    let token  = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwibmFtZWlkIjoiNjIzNWEyMDEtMDQzYi00MTJhLTgyMWQtMDkyODNhNDA5ZmE3IiwiZ3JvdXBzaWQiOiJtYW5hZ2VtZW50LXByb2R1Y3RzLWRldmVsb3BtZW50IiwibmJmIjoxNzQ5NjIzOTk5LCJleHAiOjE3NDk2MjU3OTksImlhdCI6MTc0OTYyMzk5OSwiaXNzIjoiTWFuYWdlbWVudFByb2R1Y3RzQXBpIiwiYXVkIjoiTWFuYWdlbWVudFByb2R1Y3RzQXBpIn0.Yr3tocD6q__krnIB4J55CXMKbQoVdBV5fo24XIP1M5U";
    try {
        const response = fetch('https://localhost:7127/product', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}` // Aquí se agrega el token en el header
          },
          body: JSON.stringify({...data, categoryId: 1})
        });
        if (!response.ok) {
          throw new Error('Error al cargar las facturas');
        }
        setFeedbackModalOpen(true);
      } catch (err) {
        setError(err.message); 
      }
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

  const handleSelectedItems = (selectedItems) => {
    setSelectedItems([...selectedItems]);
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
            <InvoicesTable selectedItems={handleSelectedItems} />

            {/* SuccessMod */}
            <ModalBasic id="basic-modal" modalOpen={basicModalOpen} setModalOpen={setBasicModalOpen} title="Basic Modal">

              {/* Modal content */}
              <div className="px-5 pt-4 pb-1">
                <div className="text-sm">
                  <div className="font-medium text-gray-800 dark:text-gray-100 mb-2">Let’s Talk Paragraph</div>
                  <div className="space-y-2">
                    <p>
                      Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna
                      aliqua.
                    </p>
                    <p>
                      Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint
                      occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                    </p>
                  </div>
                </div>
              </div>
              
              {/* Modal footer */}
              <div className="px-5 py-4">
                <div className="flex flex-wrap justify-end space-x-2">
                  <button
                    className="btn-sm border-gray-200 dark:border-gray-700/60 hover:border-gray-300 dark:hover:border-gray-600 text-gray-800 dark:text-gray-300"
                    onClick={(e) => {
                      e.stopPropagation();
                      setBasicModalOpen(false);
                    }}
                  >
                    Close
                  </button>
                  <button className="btn-sm bg-gray-900 text-gray-100 hover:bg-gray-800 dark:bg-gray-100 dark:text-gray-800 dark:hover:bg-white">I Understand</button>
                </div>
              </div>
            </ModalBasic>
            
          </div>
        </main>
      </div>
    </div>
  );
}

export default Invoices;