import { defaultOng, Ong } from './ong'

export interface Pet {
  id: number
  breed: string
  color: string
  size: string
  sex: string
  personality?: string
  wasAdopted: boolean
  photo: string
  ongId: number
  ong: Ong
}

export const defaultPet = (): Pet => {
  return {
    id: 0,
    breed: '',
    color: '',
    size: '',
    sex: '',
    personality: '',
    wasAdopted: false,
    photo: '',
    ongId: 0,
    ong: defaultOng(),
  }
}
