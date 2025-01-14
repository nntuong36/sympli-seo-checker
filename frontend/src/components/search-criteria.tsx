import { FunctionComponent, useMemo, useState } from "react";
import { IRankingRequest } from "../types/ranking-request";
import MultiCheckboxs from "./multi-checkboxs";
import { ICheckboxData } from "../types/checkbox-data";
import { TextboxInput } from "./textbox-input";
import { toast } from "react-toastify";
import {
  arrayFind,
  arrayPush,
  isNullOrEmpty,
  isValidUrl,
} from "../core/utilities";
import { ErrorItem } from "../types/error-item";
import {
  KeywordIsRequired,
  KeywordLengthMustBeInRange,
  SearchCriteriaNotValid,
  SearchEngineIsRequired,
  UrlIsNotValid,
  UrlIsRequired,
} from "../core/error-constants";
import ButtonCOntrol from "./button-control";

interface SearchCriteriaProps {
  onSubmitData: (data: IRankingRequest) => void;
  isLoading: boolean;
}

const validateData = (model: IRankingRequest): ErrorItem[] => {
  const errors: ErrorItem[] = [];
  if (isNullOrEmpty(model.keyword)) {
    arrayPush(errors, {
      path: "keyword",
      message: KeywordIsRequired,
    });
  }

  if (model.keyword?.length < 3 || model.keyword?.length > 100) {
    arrayPush(errors, {
      path: "keyword",
      message: KeywordLengthMustBeInRange,
    });
  }

  if (isNullOrEmpty(model.url)) {
    arrayPush(errors, { path: "url", message: UrlIsRequired });
  }

  if (isNullOrEmpty(model.searchEngines) || model.searchEngines.length === 0) {
    arrayPush(errors, {
      path: "searchEngines",
      message: SearchEngineIsRequired,
    });
  }

  if (!isValidUrl(model.url)) {
    arrayPush(errors, { path: "url", message: UrlIsNotValid });
  }

  return errors;
};

const SearchCriteria: FunctionComponent<SearchCriteriaProps> = (props) => {
  const { onSubmitData, isLoading } = props;
  const [requestInfo, setRequestInfo] = useState<IRankingRequest>({
    keyword: "",
    url: "",
    searchEngines: [],
  });
  const [errors, setErrors] = useState<ErrorItem[]>();

  const searchEngineItems = useMemo<ICheckboxData[]>(() => {
    // TODO: can fetch from api
    return [
      {
        id: 0,
        text: "Google",
      },
      {
        id: 1,
        text: "Bing",
      },
    ];
  }, []);

  const handleChangeModelValue = (
    key: string,
    valueChange: string | number[]
  ) => {
    setRequestInfo((prev) => {
      return {
        ...prev,
        [key]: valueChange,
      };
    });
    setErrors([]);
  };

  const handleOnSubmit = () => {
    const errors = validateData(requestInfo);
    setErrors(errors);

    if (errors?.length === 0) {
      onSubmitData(requestInfo);
    } else {
      toast(SearchCriteriaNotValid);
    }
  };

  const error = useMemo<{
    keyword: string;
    url: string;
    searchEngines: string;
  }>(() => {
    return {
      keyword: arrayFind(errors, (e: ErrorItem) => e.path === "keyword")
        ?.message,
      url: arrayFind(errors, (e: ErrorItem) => e.path === "url")?.message,
      searchEngines: arrayFind(
        errors,
        (e: ErrorItem) => e.path === "searchEngines"
      )?.message,
    };
  }, [errors]);

  return (
    <div>
      <TextboxInput
        initialValue={requestInfo?.keyword}
        label="Keyword (eg. e-settlements)"
        placeholder="Please input keyword"
        onValueChanged={(nextVal) => {
          handleChangeModelValue("keyword", nextVal);
        }}
        errorContent={error?.keyword}
        debounceInterval={500}
        maxLength={100}
      />
      <TextboxInput
        initialValue={requestInfo?.url}
        label="Url (eg. www.sympli.com.au) "
        placeholder="Please input url"
        onValueChanged={(nextVal) => {
          handleChangeModelValue("url", nextVal);
        }}
        errorContent={error?.url}
        debounceInterval={500}
        maxLength={100}
      />
      <MultiCheckboxs
        checkboxDataItems={searchEngineItems}
        onValueChanged={(nextVal) => {
          handleChangeModelValue("searchEngines", nextVal);
        }}
        label="Search engines"
        errorContent={error?.searchEngines}
      />
      <ButtonCOntrol
        onSubmit={() => handleOnSubmit()}
        isLoading={isLoading}
        label="Submit"
      />
    </div>
  );
};

export default SearchCriteria;
