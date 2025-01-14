import { FunctionComponent, useState } from "react";
import SearchCriteria from "../components/search-criteria";
import { IRankingRequest } from "../types/ranking-request";
import SearchResult from "../components/search-result";
import { IRankingResult } from "../types/ranking-result";
import { getSearchRankingApi } from "../services/search-ranking.service";

interface ISearchRankingPageProps {
  pageTitle: string;
}

const SearchRankingPage: FunctionComponent<ISearchRankingPageProps> = (
  props
) => {
  const { pageTitle } = props;
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [rankingResults, setRankingResults] = useState<IRankingResult[]>([]);

  const onSubmitData = async (data: IRankingRequest) => {
    setIsLoading(true);

    const response = await getSearchRankingApi(data);
    setRankingResults(response ?? []);
    
    setIsLoading(false);
  };

  return (
    <div>
      <h4>{pageTitle}</h4>
      <SearchCriteria onSubmitData={(data) => onSubmitData(data)} isLoading={isLoading} />
      <SearchResult rankingResults={rankingResults} />
    </div>
  );
};

export default SearchRankingPage;
