export interface Search {
  type: string
  breed?: string
  color?: string
  size: string
  sex: 'F' | 'M'
  personality?: string
  userId: number
}

export const defaultSearch = (): Search => {
  return {
    type: '',
    sex: 'F',
    size: '',
    userId: 0,
  }
}
