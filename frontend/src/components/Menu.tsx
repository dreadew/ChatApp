import { ChatBubbleIcon, ExitIcon, PersonIcon } from '@radix-ui/react-icons'
import { MenuLink } from './MenuLink'
import { useContext } from 'react'
import { AuthContext } from '../contexts/auth-context'
import { MenuButton } from './MenuButton'


export const Menu = () => {
  const { logout } = useContext(AuthContext)

  const handleLogout = () => {
    logout()
    window.location.reload()
  }

  return <nav className='fixed bottom-0 border-t border-t-accent h-16 w-full flex gap-24 items-center justify-center'>
    <MenuLink text={<>Пользователи</>} icon={<PersonIcon className='h-6 w-6 bg-background' />} href={'/users'} />
    <MenuLink text={<>Чаты</>} icon={<ChatBubbleIcon className='h-6 w-6 bg-background' />} href={'/chats'} />
    <MenuButton text={<>Выйти из аккаунта</>} icon={<ExitIcon className='h-6 w-6 bg-background' />} action={handleLogout} />
  </nav>
}