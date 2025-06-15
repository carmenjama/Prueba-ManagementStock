import React, { useState, useEffect } from 'react';
import Invoices from './InvoicesTableItem';
import PaginationClassic from '../../components/PaginationClassic';

function InvoicesTable({
  selectedItems
}) {
  const [loading, setLoading] = useState(true);  // Para manejar el estado de carga
  const [error, setError] = useState(null);      // Para manejar errores
  const [isCheck, setIsCheck] = useState([]);
  const [list, setList] = useState([]);
  const [page, SetPage] = useState({
    currentPage : 1,
    limit: 2
  });

  useEffect(() => {
    let token  = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwibmFtZWlkIjoiNDliMzZlNzYtZDVhNC00NGJiLTkyZTEtZWQxZDFmMTY2ZTQ5IiwiZ3JvdXBzaWQiOiJtYW5hZ2VtZW50LXByb2R1Y3RzLWRldmVsb3BtZW50IiwibmJmIjoxNzUwMDAzOTQ4LCJleHAiOjE3NTAwMDU3NDgsImlhdCI6MTc1MDAwMzk0OCwiaXNzIjoiTWFuYWdlbWVudFByb2R1Y3RzQXBpIiwiYXVkIjoiTWFuYWdlbWVudFByb2R1Y3RzQXBpIn0.vFyJ6msgvFF4fhxrnthlUSWmwObDsnggXK9wvCeqejM";
    const fetchInvoices = async () => {
      try {
        const response = await fetch('https://localhost:7127/product/true?page='+page.currentPage+'&limit='+page.limit, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}` // Aquí se agrega el token en el header
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
    };

    fetchInvoices();
  }, [page.currentPage]);

  const handleSelectAll = () => {
    setSelectAll(!selectAll);
    setIsCheck(list.map(li => li.id));
    if (selectAll) {
      setIsCheck([]);
    }
  };

  const handleClick = e => {
    const { id, checked } = e.target;
    setSelectAll(false);
    setIsCheck([...isCheck, id]);
    if (!checked) {
      setIsCheck(isCheck.filter(item => item !== id));
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

  useEffect(() => {
    selectedItems(isCheck);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isCheck]);

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
                <th className="w-100 px-2 first:pl-5 last:pr-5 py-3 whitespace-nowrap">
                  <div className="font-semibold text-left">Notas</div>
                </th>
                <th className="px-2 first:pl-5 last:pr-5 py-3 whitespace-nowrap">
                  <div className="font-semibold text-left">Actions</div>
                </th>
              </tr>
            </thead>
            {/* Table body */}
            <tbody className="text-sm divide-y divide-gray-100 dark:divide-gray-700/60">
              {
                list.map(invoice => {
                  return (
                    <Invoices
                      id={invoice.id}
                      categoryid={invoice.categoryId}
                      code={invoice.code}
                      name={invoice.name}
                      categoryname={invoice.categoryName}
                      price={invoice.price}
                      stock={invoice.stock}
                      unit={invoice.unit}
                      note={invoice.note}
                      hasmultimedia={invoice.hasMultimedia}
                      handleClick={handleClick}
                      isChecked={isCheck.includes(invoice.id)}
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
    </div>
  );
}

export default InvoicesTable;
