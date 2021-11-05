import { Module } from 'vuex'

interface State {
  isLoading: boolean
  message: string
}

const module: Module<State, unknown> = {
  state: {
    isLoading: false,
    message: '',
  },
  mutations: {
    startLoading(state, message: string) {
      state.message = message
      state.isLoading = true
    },
    stopLoading(state) {
      state.isLoading = false
      state.message = ''
    },
  },
  namespaced: true,
}

export default module
