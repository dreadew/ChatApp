import { useContext, useEffect } from 'react'
import { SignInForm } from './forms/sign-in-form'
import { H3 } from './typography/h3'
import { Logo } from './ui/logo'
import { Link, useNavigate } from 'react-router-dom'
import { AuthContext } from '../contexts/auth-context'

export const SignInPage = () => {
  const { isAuthorized } = useContext(AuthContext)
  const navigate = useNavigate()

  useEffect(() => {
    if (isAuthorized) {
      navigate('/users')
    }
  }, [isAuthorized])

  return <div className='h-screen flex flex-col gap-8 items-center justify-center'>
    <div className='flex flex-col items-center gap-4'>
      <Link to={'/'}>
        <Logo />
      </Link>
      <H3 text={"Вход в аккаунт"} />
    </div>
    <SignInForm />
  </div>
}