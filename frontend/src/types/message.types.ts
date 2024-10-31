export type MessageResponse = {
  id: string
  senderId: string
  content: string
  chatId: string
  createdAt: string
  updatedAt: string
}

export type CreateMessageRequest = {
  senderId: string
  content: string
  chatId: string
}

export type CreateMessageResponse = {
  id: string
}

export type UpdateMessageRequest = {
  id: string
  content: string
  senderId: string
}

export type DeleteMessageRequest = {
  id: string
  senderId: string
}

export type MessageInfo = {
  userId: string
  username: string
  messageId: string
  message: string
}