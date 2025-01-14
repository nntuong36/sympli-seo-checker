import axios, { AxiosError } from "axios";
import { AccessDeniedError, AuthenticationError, NotFoundError } from "../exception/access-denined.error";

export const HandleResponseError = <T>(statusCode: number, action: (resp?: AxiosError<T, any>) => AxiosError<T>) => {
    return (err: AxiosError<T, any>) => {
      if (!axios.isCancel(err as any) && action && err.response && err.response.status === statusCode) {
        return action(err);
      }
      throw err;
    };
  };
  
  export const Http400HandlerDefault = HandleResponseError<any>(400, (err) => {
    return {
      data: {
        IsError: true,
        Data: err?.response?.data,
      },
      err,
    } as any;
  });
  
  export const Http403HandlerDefault = HandleResponseError<any>(403, () => {
    throw new AccessDeniedError('Access denied!', '/');
  });
  
  export const Http401HandlerDefault = HandleResponseError<any>(401, () => {
    throw new AuthenticationError('Please login!', '/');
  });

  export const Http404HandlerDefault = HandleResponseError<any>(404, () => {
    throw new NotFoundError('Not found');
  });
  
  export const Http500HandlerDefault = HandleResponseError<any>(500, (err) => {
    return {
      data: {
        IsError: true,
        Data: err?.response?.data,
      },
      err,
    } as any;
  });
  