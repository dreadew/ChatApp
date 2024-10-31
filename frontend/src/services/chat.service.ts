import { axiosInstance } from '../api/axios'
import { ChatResponse, CreateChatRequest, CreateChatResponse, DeleteChatRequest, FindOrCreatePrivateChatRequest, FindOrCreatePrivateChatResponse, GetChatRequest, UpdateChatRequest } from '../types/chat.types'

class ChatService {
  private URL = import.meta.env.VITE_API_URL + "/api/Chat"

  async Create(data: CreateChatRequest) {
    return axiosInstance.post<CreateChatResponse>(this.URL, data)
  }

  async Get(data: GetChatRequest) {
    return axiosInstance.get<ChatResponse>(this.URL + `/${data.id}`)
  }

  async Update(data: UpdateChatRequest) {
    return axiosInstance.patch(this.URL, data)
  }

  async Delete(data: DeleteChatRequest) {
    return axiosInstance.delete(this.URL + `?id=${data.id}`)
  }

  async FindOrCreatePrivateChat(data: FindOrCreatePrivateChatRequest) {
    return axiosInstance.post<FindOrCreatePrivateChatResponse>(this.URL + '/find', data)
  }
}

export default new ChatService()
