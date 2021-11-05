export interface Pet {
  id: number
  breed: string
  color: string
  size: string
  sex: string
  personality: string
  wasAdopted: boolean
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
  }
}
