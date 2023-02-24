import axios from "axios";
const baseUrl = process.env.REACT_APP_API_SERVER_URL
const usersUrl = `${baseUrl}/api/users`

const getAll = async (token) => {
  const config = {
    headers: {
      "content-type": "application/json",
      Authorization: `Bearer ${token}`,
    }
  };
  const response = await axios.get(usersUrl, config);
  return response.data;
}

const usersService = { getAll };
export default usersService
