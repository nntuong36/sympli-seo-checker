import { AxiosRequestConfig } from "axios";
import axiosInstance from "./axios-service";
import {
  Http400HandlerDefault,
  Http401HandlerDefault,
  Http403HandlerDefault,
  Http404HandlerDefault,
  Http500HandlerDefault,
} from "../core/http/http-error-handlers";
import { toast } from "react-toastify";

type HttpMethods =
  | "GET"
  | "HEAD"
  | "POST"
  | "PUT"
  | "DELETE"
  | "OPTIONS"
  | "PATCH";

const httpRequestRaw = (
  url: string,
  method: HttpMethods,
  data?: any,
  config?: AxiosRequestConfig
) => {
  const requestConfig = {
    url,
    method,
    data,
    params: data,
    ...{
      ...config,
      headers: {
        "content-type": "application/json",
        ...(config?.headers || {}),
      },
    },
  } as AxiosRequestConfig<any>;
  return axiosInstance.request(requestConfig);
};

const httpRequest = (
  url: string,
  method: HttpMethods,
  data: any,
  config?: AxiosRequestConfig
) => {
  return httpRequestRaw(url, method, data, config)
    .catch(Http400HandlerDefault)
    .catch(Http403HandlerDefault)
    .catch(Http401HandlerDefault)
    .catch(Http404HandlerDefault)
    .catch(Http500HandlerDefault);
};

export const CommonGet = <TResponse = any>(
  url: string,
  data?: any
): Promise<TResponse> =>
  httpRequest(url, "GET", data, {
    paramsSerializer: {
      indexes: null,
    },
  }).then((resp: any) => {
    const responseData = resp?.data;
    if (responseData?.IsError) {
      toast(responseData.Data?.message);
      return undefined;
    }
    return responseData;
  });
