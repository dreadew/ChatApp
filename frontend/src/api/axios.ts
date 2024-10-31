import axios, { CreateAxiosDefaults } from 'axios'
import { GetAccessToken } from '../services/cookies.service'

const options: CreateAxiosDefaults = {
  headers: {
    'Content-Type': 'application/json'
  }
}

const axiosInstance = axios.create(options)

axiosInstance.interceptors.request.use(
	async config => {
		const accessToken = GetAccessToken()

		if (accessToken !== undefined) {
			config.headers.Authorization = `Bearer ${accessToken}`
		}

		return config
	},
	error => Promise.reject(error)
)

axiosInstance.interceptors.response.use(
	response => response,
	async error => {
		const config = error?.config

		if (
			(error?.response?.status === 401 && !config.sent) ||
			error?.response?.data?.message === 'AccessToken has expired'
		) {
			config.sent = true
		}

		return Promise.reject(error)
	}
)

export { axiosInstance }