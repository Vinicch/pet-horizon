import { defaultOng, Ong } from './ong'
import { defaultPet, Pet } from './pet'
import { defaultUser, User } from './user'

export interface Adoption {
  id: number
  situation: 'A' | 'R' | 'W'
  disapprovalReason: string
  ongId: number
  ong: Ong
  petId: number
  pet: Pet
  userId: number
  user: User
}

export const defaultAdoption = (): Adoption => {
  return {
    id: 0,
    situation: 'W',
    disapprovalReason: '',
    ongId: 0,
    ong: defaultOng(),
    petId: 0,
    pet: defaultPet(),
    userId: 0,
    user: defaultUser(),
  }
}
