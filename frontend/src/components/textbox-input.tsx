import React, {
  useEffect,
  useState,
  useCallback,
  useMemo,
} from 'react';
import '../css/textbox-input.css';
import { debounce } from 'lodash-es';

export interface TextboxInputProps {
  initialValue: string;
  label: string;
  onValueChanged: (value: string) => void;
  placeholder?: string;
  maxLength?: number;
  disabled?: boolean;
  errorContent?: string;
  debounceInterval?: number;
}

export const TextboxInput = (props: TextboxInputProps) => {
  const {
    initialValue,
    label,
    onValueChanged,
    placeholder,
    maxLength,
    disabled = false,
    errorContent,
    debounceInterval,
  } = props;
  const [value, setValue] = useState<string>('');

  useEffect(() => {
    setValue(initialValue);
  }, [initialValue]);

  const onValueChangedDebounce = useMemo(
    () =>
      onValueChanged ? debounce(onValueChanged, debounceInterval) : undefined,
    [onValueChanged, debounceInterval]
  );

  const handleInputValueChanged = useCallback((event: React.ChangeEvent<HTMLInputElement>) => {
    setValue(event.target.value);
    onValueChangedDebounce &&
      onValueChangedDebounce(event.target.value);
  }, []);

  return (
    <div className="textbox-input">
      <label>{label}</label>
      <input
        className={errorContent && 'error'}
        type="text"
        value={value}
        onChange={handleInputValueChanged}
        placeholder={placeholder}
        disabled={disabled}
        maxLength={maxLength}
      />
      {errorContent && <div className='error-text'>{errorContent}</div>}
    </div>
  );
};
