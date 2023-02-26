const isStr = (text: unknown): text is string =>
  typeof text === "string" || text instanceof String;

const isBool = (bool: unknown): bool is boolean =>
  typeof bool === "boolean" || bool instanceof Boolean;

const isNum = (num: unknown): num is number =>
  typeof num === "number" || num instanceof Number;

const str = (input: string | unknown): string => {
  if (!isStr(input) || !input) {
    throw new Error(`input:${input} is not a string`);
  } else {
    return input;
  }
};

const bool = (input: boolean | unknown): boolean => {
  if (!isBool(input)) {
    throw new Error(`input:${input} is not a boolean`);
  } else {
    return input;
  }
};

const num = (input: number | unknown): number => {
  if (!isNum(input)) {
    throw new Error(`input:${input} is not a number`);
  } else {
    return input;
  }
};

const mongoId = (input: string | unknown): string => {
  const objectIdRegex = /^[0-9a-fA-F]{24}$/;
  if (!isStr(input) || !input.match(objectIdRegex)) {
    throw new Error(`input:${input} is not a valid mongo id`);
  } else {
    return input;
  }
};

const parse = {
  str,
  mongoId,
  bool,
  num,
  isNum,
};

export default parse;
