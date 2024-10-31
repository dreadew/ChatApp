import {createContext, ReactNode, useEffect, useState} from 'react'
import { GetAccessToken, RemoveAccessToken } from '../services/cookies.service'
import { decodeJwt } from 'jose'

type AuthContextType = {
  token: string | null
  isAuthorized: boolean
  logout: () => void
}

export const AuthContext = createContext<AuthContextType>({
  token: null,
  isAuthorized: false,
  logout: () => {}
})

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [token, setToken] = useState<string | null>(null)
  const accessToken = GetAccessToken()

  useEffect(() => {
    const updateToken = async (token: string) => {
      if (await decodeJwt(token)) {
        setToken(token)
      }
    }

    if (accessToken != null) {
      updateToken(accessToken)
    }
  }, [accessToken])

  const logout = () => {
    RemoveAccessToken();
  }

  const value = {
    token,
    isAuthorized: token !== null,
    logout
  }

  return <AuthContext.Provider value={value}>
    {children}
  </AuthContext.Provider>
}