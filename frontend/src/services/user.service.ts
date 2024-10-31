import { axiosInstance } from '../api/axios'
import { CreateUserRequest, CreateUserResponse, DeleteUserRequest, GetUserRequest, ListUserRequest, LoginUserRequest, LoginUserResponse, UpdateUserRequest, UserResponse, UsersResponse } from '../types/user.types'

class UserService {
  private URL = import.meta.env.VITE_API_URL + "/api/User"

  async SignIn(data: LoginUserRequest) {
    return axiosInstance.post<LoginUserResponse>(this.URL + '/login', data)
  }

  async SignUp(data: CreateUserRequest) {
    return axiosInstance.post<CreateUserResponse>(this.URL + '/create', data)
  }

  async Get(data: GetUserRequest) {
    return axiosInstance.get<UserResponse>(this.URL + `/${data.id}`)
  }

  async List(data: ListUserRequest) {
    return axiosInstance.get<UsersResponse>(this.URL + '/list' + `?take=${data.take}&skip=${data.skip}`)
  }

  async Update(data: UpdateUserRequest) {
    return axiosInstance.patch(this.URL, data)
  }

  async Delete(data: DeleteUserRequest) {
    return axiosInstance.delete(this.URL + `?id=${data.id}`)
  }
}

export default new UserService()