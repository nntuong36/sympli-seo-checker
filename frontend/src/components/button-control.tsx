import { FunctionComponent } from "react";
import "../css/button-control.css";

interface ButtonControlProps {
  onSubmit(): void;
  isLoading?: boolean;
  label?: string;
}

const ButtonCOntrol: FunctionComponent<ButtonControlProps> = (props) => {
  const { onSubmit, isLoading, label } = props;

  const handleOnClick = (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
    event.preventDefault();
    onSubmit();
  }

  return (
    <button
      disabled={isLoading === true}
      onClick={(event) => handleOnClick(event)}
    >
      {isLoading ? "Loading..." : label ?? "Submit"}
    </button>
  );
};

export default ButtonCOntrol;
