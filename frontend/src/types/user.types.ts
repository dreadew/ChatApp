import { ChatInfoWithoutUsers } from './chat.types'

export type UserResponse = {
  data: UserInfo
}

export type UserInfo = {
  id: string
  username: string
  email: string
  createdAt: string
  updatedAt: string
  chats: ChatInfoWithoutUsers[]
}

export type UsersResponse = {
  data: UserInfo[]
}

export type CreateUserRequest = {
  username: string
  email: string
  password: string
}

export type CreateUserResponse = {
  data: {
    id: string
  }
}

export type LoginUserRequest = {
  username: string
  password: string
}

export type LoginUserResponse = {
  data: {
    accessToken: string
  }
}

export type GetUserRequest = {
  id: string
}

export type ListUserRequest = {
  take: number
  skip: number
}

export type UpdateUserRequest = {
  id: string
}

export type DeleteUserRequest = {
  id: string
}