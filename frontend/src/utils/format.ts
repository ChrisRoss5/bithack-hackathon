export function formatDate(date?: Date) {
  if (!date) return undefined;
  return (
    date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate()
  );
}
