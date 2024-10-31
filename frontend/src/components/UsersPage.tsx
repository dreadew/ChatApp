import { useContext, useEffect, useState } from 'react'
import { UserInfo } from '../types/user.types'
import userService from '../services/user.service'
import { Menu } from './Menu'
import { H2 } from './typography/h2'
import { Container } from './Container'
import { PersonIcon } from '@radix-ui/react-icons'
import { useNavigate } from "react-router-dom"
import { DecodeToken, GetAccessToken } from '../services/cookies.service'
import chatService from '../services/chat.service'
import { AuthContext } from '../contexts/auth-context'

export const UsersPage = () => {
  const [users, setUsers] = useState<UserInfo[]>([])
  const [userId, setUserId] = useState<string>()
  const token = GetAccessToken()
  const { isAuthorized } = useContext(AuthContext)
  const navigate = useNavigate()

  useEffect(() => {
    if (!isAuthorized) {
      navigate("/")
    }
  }, [isAuthorized, navigate])

  useEffect(() => {
    const fetchUsers = async () => {
      const res = await userService.List({take: 10, skip: 0})
      setUsers(res.data.data)
      const decodedToken = await DecodeToken(token!)
      setUserId(decodedToken.userId)
    }
    fetchUsers()
  }, [])

  const handleFindOrCreateChat = async (withUserId: string) => {
    const res = await chatService.FindOrCreatePrivateChat({ usersIds: [userId!, withUserId] })
    navigate(`/chat/${res.data.data.id}`)
  }

  return <>
    <Container>
      <div className='h-screen flex flex-col gap-6'>
        <H2 text={"Список пользователей"} />
        <ul className='mt-6 w-full flex flex-col gap-3'>
          {
            users.filter(u => u.id !== userId).map((user, idx) => (
              <button className='w-full' onClick={async () => handleFindOrCreateChat(user.id)} key={`user-${idx}`}>
                <li className='p-2 flex items-center justify-between font-medium text-foreground transition-all betterhover:hover:opacity-80 betterhover:hover:bg-accent/50 rounded-lg'>
                  <div className='flex items-center gap-6'>
                    <PersonIcon className='h-6 w-6 text-accent-foreground' />
                    {user.username}
                  </div>
                  <span className='text-xs text-accent-foreground'>
                    {user.createdAt.split('T')[0]}
                  </span>
                </li>
              </button>
              )
            )
          }
        </ul>
      </div>
    </Container>
    <Menu />
  </>
}