import { UserResponse } from './user.types'

export type ChatResponse = {
  data: ChatInfo
}

export type ChatsResponse = {
  data: ChatInfo[]
}

export type ChatInfo = {
  id: string
  name: string
  isGroupChat: boolean
  users: UserResponse[]
  createdAt: string
  updatedAt: string
}

export type ChatInfoWithoutUsers = Omit<ChatInfo, "users">

export type CreateChatRequest = {
  name: string
  isGroupChat: boolean
  usersIds: string[]
}

export type CreateChatResponse = {
  data: {
    id: string
  }
}

export type GetChatRequest = {
  id: string
}

export type UpdateChatRequest = {
  id: string
  name: string
  creatorId: string
  usersIds: string[]
}

export type DeleteChatRequest = {
  id: string
  creatorId: string
}

export type FindOrCreatePrivateChatRequest = {
  usersIds: string[]
}

export type FindOrCreatePrivateChatResponse = {
  data: {
    id: string
  }
}