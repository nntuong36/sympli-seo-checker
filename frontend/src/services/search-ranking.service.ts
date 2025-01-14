import { IRankingRequest } from "../types/ranking-request";
import { IRankingResult } from "../types/ranking-result";
import { CommonGet } from "./base.service";

export const getSearchRankingApi: (
  params: IRankingRequest
) => Promise<IRankingResult[]> = (params) => {
  return CommonGet<IRankingResult[]>("/api/search/ranking", params)
};
