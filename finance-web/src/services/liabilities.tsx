import axios from "axios";
import { Liability, NewLiability } from "../types";
const baseUrl = process.env.REACT_APP_API_SERVER_URL;
const liabilitiesUrl = `${baseUrl}/api/Liabilities`;

const getAll = async (token: string) => {
  console.log(token);
  const config = {
    headers: {
      "content-type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  };

  const response = await axios.get(liabilitiesUrl, config);
  return response.data;
};

interface CreateLiab {
  liability: NewLiability;
  token: string;
}

const create = async (createLiab: CreateLiab) => {
  const config = {
    headers: {
      "content-type": "application/json",
      Authorization: `Bearer ${createLiab.token}`,
    },
  };

  const response = await axios.post(
    liabilitiesUrl,
    createLiab.liability,
    config
  );
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

const liabilitiesService = { getAll, create, update, remove };

export default liabilitiesService;
