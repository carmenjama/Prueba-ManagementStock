
export const setToken = async () => {
  try {
    const response = await fetch(getUrl("auth"), {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json-patch+json',
      },
      body: JSON.stringify({ user: "admin", pass: "16073a5bbae5f899b3f55b4e533e156a" })
    });

    if (!response.ok) {
      throw new Error("Error al obtener el token");
    }

    const result = await response.json(); 
    localStorage.setItem("token", result);
  } catch (err) {
    
  }
};
export const getToken = () => {
  return `Bearer ${localStorage.getItem('token')}`;
};

export const setUrl = () => {
  localStorage.setItem('url', 'https://localhost:7127/');
};

export const getUrl = (api) => {
  return localStorage.getItem('url') + api;
};