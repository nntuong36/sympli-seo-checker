import axios, { AxiosInstance } from 'axios';
import environment from '../environment';
export const DefaultHttpAbortTimeoutMs = 20000; // 20 seconds

const axiosInstance: AxiosInstance = axios.create({
  baseURL: environment.host,
  timeout: DefaultHttpAbortTimeoutMs,
});

export default axiosInstance;