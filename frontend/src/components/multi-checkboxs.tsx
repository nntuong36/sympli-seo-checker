import { FunctionComponent, useCallback, useEffect, useState } from "react";
import { ICheckboxData } from "../types/checkbox-data";
import "../css/multi-checkboxs.css";

interface MultiCheckboxsProps {
  checkboxDataItems: ICheckboxData[];
  onValueChanged(ids: number[]): void;
  errorContent?: string;
  label?: string;
}

const MultiCheckboxs: FunctionComponent<MultiCheckboxsProps> = (props) => {
  const { checkboxDataItems, onValueChanged, errorContent, label } = props;

  const [selectedIds, setSelectedIds] = useState<number[]>([]);
  const [isSelectAll, setIsSelectAll] = useState<boolean>(false);

  useEffect(() => {
    onValueChanged(selectedIds);
  }, [selectedIds]);

  const handleCheckboxChange = useCallback(
    (event: React.ChangeEvent<HTMLInputElement>) => {
      const checkedId = +event.target.value;

      let selectingIds: number[] = [];
      if (event.target.checked) {
        selectingIds = [...selectedIds, checkedId];
      } else {
        selectingIds = selectedIds.filter((id) => id !== checkedId);
      }

      setSelectedIds(selectingIds);
      setIsSelectAll(false || selectingIds.length === checkboxDataItems.length);
    },
    [selectedIds, checkboxDataItems.length]
  );

  const handleSelectAllCheckboxs = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const allIds = checkboxDataItems.map((d) => d.id);

    setIsSelectAll(event.target.checked);
    setSelectedIds(event.target.checked ? allIds : []);
  };

  return (
    <div className="multi-checkbox">
      <label>
        <input
          type="checkbox"
          checked={isSelectAll}
          onChange={(event) => {
            handleSelectAllCheckboxs(event);
          }}
        />
        {label}
      </label>
      <div className="checkbox-item">
        {checkboxDataItems.map((item, index) => {
          return (
            <label key={index}>
              <input
                type="checkbox"
                value={item.id}
                checked={selectedIds.includes(item.id)}
                onChange={(event) => {
                  handleCheckboxChange(event);
                }}
              />
              {item.text}
            </label>
          );
        })}
      </div>
      {errorContent && <span className="error-text">{errorContent}</span>}
    </div>
  );
};

export default MultiCheckboxs;
