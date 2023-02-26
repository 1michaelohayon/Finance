import React, { useState } from "react";
export const useField = (type: string) => {
  const [notify, setNotify] = useState("");
  const [value, setValue] = useState<string>("");
  const onChange = (
    event:
      | React.FormEvent<HTMLInputElement>
      | React.FormEvent<HTMLTextAreaElement>
  ) => {
    setValue((event.target as HTMLInputElement).value);
  };

  let timeoutID: ReturnType<typeof setTimeout>;
  const setNotification = (message: string) => {
    clearTimeout(timeoutID);

    setNotify(message);
    timeoutID = setTimeout(() => {
      setNotify(""); // reset notify
    }, 7000);
  };

  const reset = () => setValue("");
  return {
    reset: reset,
    input: {
      type,
      value,
      onChange,
    },
    notify,
    setNotification,
  };
};
export default useField;
