export const isNullOrEmpty = (value: any): boolean =>
  value === "" || value === null || value === undefined;

export const arrayPush = (arr: any[], ...args: any[]): any[] => {
  [].push.apply(arr, args as never[]);
  return arr;
};

export const arrayFind = (
  list: any[] | null | undefined,
  pred: (val: any, index: number) => boolean
): any | undefined => (list || []).find(pred);

export const isValidUrl = (url: string): boolean => {
  const pattern = new RegExp(
    "^(https?:\\/\\/)?((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{2,})",
    "i"
  );
  return !!pattern.test(url);
};
