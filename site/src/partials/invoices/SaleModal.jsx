import React, { useState } from "react";
import ModalBasic from "../../components/ModalBasic"; 
import { getUrl, getToken } from "../../utils/Auth";

function SaleModal({ modalOpen, setModalOpen, productId, onSaleSuccess }) {
    if (!modalOpen) return null;
  const [formData, setFormData] = useState({
    quantity: 1,
    date: new Date().toISOString().slice(0, 10),
    note: "",
  });
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(false);

  const handleChange = (e) => {
    const { id, value } = e.target;
    setFormData((prev) => ({ ...prev, [id]: value }));
  };

  const handleSubmit = async () => {
    setError(null);
    setLoading(true);
    try {
    //   const response = await fetch(getUrl("sales"), {
    //     method: "POST",
    //     headers: {
    //       "Content-Type": "application/json",
    //       Authorization: getToken(),
    //     },
    //     body: JSON.stringify({
    //       productId,
    //       quantity: parseInt(formData.quantity, 10),
    //       date: formData.date,
    //       note: formData.note,
    //     }),
    //   });

    //   if (!response.ok) {
    //     const errorData = await response.json();
    //     throw new Error(errorData.message || "Error al registrar la venta");
    //   }

    //   setLoading(false);
    //   setModalOpen(false);
    //   if (onSaleSuccess) onSaleSuccess();
    } catch (err) {
    //   setLoading(false);
    //   setError(err.message);
    }
  };

  return (
    <ModalBasic
      id="sale-modal"
      modalOpen={modalOpen}
      setModalOpen={setModalOpen}
      title="Registrar Venta"
    >
      <div className="px-5 py-4">
        <div className="space-y-4">
          <div>
            <label htmlFor="quantity" className="block mb-1 font-medium">
              Cantidad <span className="text-red-500">*</span>
            </label>
            <input
              id="quantity"
              type="number"
              min="1"
              value={formData.quantity}
              onChange={handleChange}
              className="form-input w-full px-2 py-1"
              required
            />
          </div>

          <div>
            <label htmlFor="date" className="block mb-1 font-medium">
              Fecha <span className="text-red-500">*</span>
            </label>
            <input
              id="date"
              type="date"
              value={formData.date}
              onChange={handleChange}
              className="form-input w-full px-2 py-1"
              required
            />
          </div>

          <div>
            <label htmlFor="note" className="block mb-1 font-medium">
              Nota
            </label>
            <textarea
              id="note"
              rows="3"
              value={formData.note}
              onChange={handleChange}
              className="form-textarea w-full px-2 py-1"
            />
          </div>

          {error && <p className="text-red-600">{error}</p>}
        </div>
      </div>

      <div className="px-5 py-4 border-t border-gray-200 dark:border-gray-700/60 flex justify-end space-x-2">
        <button
          className="btn-sm border-gray-200 hover:border-gray-300 text-gray-800"
          onClick={() => setModalOpen(false)}
          disabled={loading}
        >
          Cancelar
        </button>
        <button
          className="btn-sm bg-gray-900 text-white hover:bg-gray-800"
          onClick={handleSubmit}
          disabled={loading}
        >
          {loading ? "Guardando..." : "Guardar"}
        </button>
      </div>
    </ModalBasic>
  );
}

export default SaleModal;
