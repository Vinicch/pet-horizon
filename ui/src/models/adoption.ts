export interface Adoption {
  id: number
  situation: 'A' | 'R' | 'W'
  disapprovalReason: string
  ongId: number
  petId: number
  userId: number
}

export const defaultAdoption = (): Adoption => {
  return {
    id: 0,
    situation: 'W',
    disapprovalReason: '',
    ongId: 0,
    petId: 0,
    userId: 0,
  }
}
