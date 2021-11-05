export interface Ong {
  id: number
  name: string
  uin: string
  address: string
  lat: number
  long: number
}

export const defaultOng = (): Ong => {
  return {
    id: 0,
    name: '',
    uin: '',
    address: '',
    lat: 0,
    long: 0,
  }
}
