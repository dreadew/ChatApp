import { create } from 'zustand'

type UpdateMessageState = {
  messageId: string | null
  messageContent: string | null
  isOpen: boolean
  open: (messageId: string, messageContent: string) => void
  close: () => void
}

export const useUpdateMessageStore = create<UpdateMessageState>()((set) => ({
  messageId: null,
  messageContent: null,
  isOpen: false,
  open: (messageId: string, messageContent: string) => set((_) => ({ isOpen: true, messageContent: messageContent, messageId: messageId})),
  close: () => set((_) => ({ isOpen: false, messageContent: null, messageId: null }))
}))