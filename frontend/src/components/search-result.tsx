import { FunctionComponent } from "react";
import { IRankingResult } from "../types/ranking-result";
import '../css/search-result.css';
import { NoSearchResultInformation } from "../core/error-constants";

interface RankingResultProps {
  rankingResults: IRankingResult[];
}

const SearchResult: FunctionComponent<RankingResultProps> = (props) => {
  const { rankingResults } = props;

  return (
    <div className="search-result">
      <h4>Result</h4>
      {rankingResults.length === 0 && <div className="information-text">{NoSearchResultInformation}</div>}
      <ul>
        {rankingResults.map((item, index) => {
          return (
            <li key={index}>
              <h5>{item.searchEngineName}</h5>
              <ul>
                {item.rankings.map((ranking, rankingIndex) => {
                  return (
                    <li key={rankingIndex}>
                      <span className="rank-number">{ranking.rank}</span>
                      <span className="url">{ranking.url}</span>
                    </li>
                  );
                })}
              </ul>
            </li>
          );
        })}
      </ul>
    </div>
  );
};

export default SearchResult;
