import { Loader } from '@googlemaps/js-api-loader'
import { apiKey } from '@/config.json'

export const getLocation = async (address: string): Promise<google.maps.LatLng> => {
  const loader = new Loader({
    apiKey,
    version: 'weekly',
  })

  await loader.load()

  const geocoder = new google.maps.Geocoder()
  const response = await geocoder.geocode({ address })

  return response.results[0].geometry.location
}
