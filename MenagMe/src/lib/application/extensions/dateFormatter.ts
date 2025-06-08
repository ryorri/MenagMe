export function formatDate(date: Date | string | undefined | null): string {
  if (!date || date === '0001-01-01T00:00:00') return ''
  const dateObj = typeof date === 'string' ? new Date(date) : date

  const day = String(dateObj.getDate()).padStart(2, '0')
  const month = String(dateObj.getMonth() + 1).padStart(2, '0')
  const year = dateObj.getFullYear()

  return `${day}-${month}-${year}`
}

export function dateToTimestamp(date: Date | string | undefined | null): number {
  if (date === undefined || date === null) {
    return 0
  }

  const dateObj = typeof date === 'string' ? new Date(date) : date
  if (isNaN(dateObj.getTime())) {
    throw new Error('Invalid date')
  }

  return dateObj.getTime()
}
