import { createStore } from 'vuex'
import loading from './modules/loading'

const store = createStore({
  modules: {
    loading,
  },
})

export default store
