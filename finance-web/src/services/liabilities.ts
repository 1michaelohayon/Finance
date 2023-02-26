import axios from "axios";
const baseUrl = process.env.REACT_APP_API_SERVER_URL;
const liabilitiesUrl = `${baseUrl}/api/liabilities`;

const getAll = async () => {
  const response = await axios.get(liabilitiesUrl);
  return response.data;
};

const getByUserId = async (id: any) => {
  const response = await axios.get(`${liabilitiesUrl}/user/${id}`);
  return response.data;
};

const create = async (newObject: any) => {
  const response = await axios.post(liabilitiesUrl, newObject);
  return response.data;
};

const update = async (id: any, newObject: any) => {
  const response = await axios.put(`${liabilitiesUrl}/${id}`, newObject);
  return response.data;
};

const remove = async (id: any) => {
  const response = await axios.delete(`${liabilitiesUrl}/${id}`);
  return response.data;
};

const liabilitiesService = { getAll, getByUserId, create, update, remove };

export default liabilitiesService;
