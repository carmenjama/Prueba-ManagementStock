import React from 'react';

function PaginationClassic({ page, handlePageChange }) {
  return (
    <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between">
      <nav className="mb-4 sm:mb-0 sm:order-1" role="navigation" aria-label="Navigation">
        <ul className="flex justify-center">
          <li className="ml-3 first:ml-0">
            Página <span className="font-medium text-gray-600 dark:text-gray-300">{page.currentPage}</span> de 
            <span className="font-medium text-gray-600 dark:text-gray-300">{page.numberPages}</span>
          </li>
          <li className="ml-3 first:ml-0">
            <button onClick={() => handlePageChange('prev')} className="btn bg-white dark:bg-gray-800 border-gray-200 dark:border-gray-700/60 hover:border-gray-300 dark:hover:border-gray-600 text-gray-800 dark:text-gray-300">← Anterior</button>
          </li>
          <li className="ml-3 first:ml-0">
            <button onClick={() => handlePageChange('next')} className="btn bg-white dark:bg-gray-800 border-gray-200 dark:border-gray-700/60 hover:border-gray-300 dark:hover:border-gray-600 text-gray-800 dark:text-gray-300">Siguiente →</button>
          </li>
        </ul>
      </nav>
      <div className="text-sm text-gray-500 text-center sm:text-left">
        Mostrar <span className="font-medium text-gray-600 dark:text-gray-300">{page.totalRecordsPage}</span> de <span className="font-medium text-gray-600 dark:text-gray-300">{page.totalRecords}</span> items
      </div>
    </div>
  );
}

export default PaginationClassic;
