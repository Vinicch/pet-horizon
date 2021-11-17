import { defaultOng, Ong } from './ong'

export interface Pet {
  id: number
  type: string
  breed: string
  color: string
  size: string
  sex: 'F' | 'M'
  personality?: string
  wasAdopted: boolean
  photo: string
  ongId: number
  ong: Ong
}

export const defaultPet = (): Pet => {
  return {
    id: 0,
    type: '',
    breed: '',
    color: '',
    size: '',
    sex: 'F',
    personality: '',
    wasAdopted: false,
    photo: '',
    ongId: 0,
    ong: defaultOng(),
  }
}
