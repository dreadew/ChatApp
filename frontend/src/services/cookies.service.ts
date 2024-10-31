import { decodeJwt, jwtVerify } from 'jose'
import Cookies from 'js-cookie'
import { AccessTokenCookie } from '../constants/cookies/access-token-cookie'
import { JWTSecret } from '../constants/cookies/jwt-secret'
import { JWTToken } from '../types/jwt.types'

export function GetAccessToken() {
  return Cookies.get(AccessTokenCookie)
}

export function SetCookie(name: string, value: string) {
  Cookies.set(name, value)
}

export function RemoveAccessToken() {
  Cookies.remove(AccessTokenCookie)
}


export async function DecodeToken(token: string) {
	return (await decodeJwt(token)) as JWTToken
}

export async function ValidateToken(token: string) {
  try {
    await jwtVerify(token, new TextEncoder().encode(JWTSecret))
    return true
  } catch (err: unknown) {
    return false
  }
}