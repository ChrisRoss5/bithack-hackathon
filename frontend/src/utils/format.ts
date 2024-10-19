export function formatDate(date?: Date) {
  if (!date) return undefined;
  return (
    date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate()
  );
}

function isIsoDateString(value: string): boolean {
  // Simple regex to check if the string matches the ISO date format
  const isoDateRegex = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}\.\d+$/;
  return isoDateRegex.test(value);
}

export function parseDates(obj: any): any {
  // If the object is an array, recursively apply the function to each element
  if (Array.isArray(obj)) {
      return obj.map(item => parseDates(item));
  }

  // If the object is not null and is of type object, recurse through its properties
  if (obj && typeof obj === 'object') {
      const parsedObj: { [key: string]: any } = {};
      for (const key in obj) {
          if (obj.hasOwnProperty(key)) {
              const value = obj[key];

              // If the value is a string and matches the date format, convert it to a Date object
              if (typeof value === 'string' && isIsoDateString(value)) {
                  parsedObj[key] = new Date(value);
              } else {
                  // Recursively parse nested objects/arrays
                  parsedObj[key] = parseDates(value);
              }
          }
      }
      return parsedObj;
  }

  // If it's neither an object nor an array, return it as is
  return obj;
}