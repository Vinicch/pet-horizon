export interface Search {
  breed?: string
  color?: string
  size: string
  sex: string
  personality?: string
  userId: number
}

export const defaultSearch = (): Search => {
  return {
    sex: '',
    size: '',
    userId: 0,
  }
}
