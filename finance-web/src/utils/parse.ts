const isStr = (text: unknown): text is string =>
  typeof text === "string" || text instanceof String;

const str = (input: string | unknown): string => {
  if (!isStr(input) || !input) {
    throw new Error(`input:${input} is not a string`);
  } else {
    return input;
  }
};

const parse = {
  str,
};

export default parse;
