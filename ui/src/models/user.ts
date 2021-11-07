export interface User {
  id: number
  name: string
  email: string
  password: string
  uin: string
  address: string
  lat: number
  long: number
  residence: string
  residenceSize: string
  hasYard: boolean
  hasWindowBars: boolean
  income: string
  ongId?: number
}

export const defaultUser = (): User => {
  return {
    id: 0,
    name: '',
    email: '',
    password: '',
    uin: '',
    address: '',
    lat: 0,
    long: 0,
    residence: '',
    residenceSize: '',
    hasYard: false,
    hasWindowBars: false,
    income: '',
  }
}
