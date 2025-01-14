import { IUrlRank } from "./url-rank";

export interface IRankingResult {
    searchEngine: number;
    searchEngineName: string;
    rankings: IUrlRank[];
}