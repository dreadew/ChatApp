import { useContext, useEffect } from 'react'
import { AuthContext } from '../contexts/auth-context'
import { SignUpForm } from './forms/sign-up-form'
import { H3 } from './typography/h3'
import { Logo } from './ui/logo'
import { Link, useNavigate } from 'react-router-dom'

export const SignUpPage = () => {
  const { isAuthorized } = useContext(AuthContext)
  const navigate = useNavigate()

  useEffect(() => {
    if (isAuthorized) {
      navigate('/users')
    }
  }, [isAuthorized])
  
  return <div className='h-screen flex flex-col gap-6 items-center justify-center'>
    <div className='flex flex-col items-center gap-4'>
      <Link to={'/'}>
        <Logo />
      </Link>
      <H3 text={"Регистрация"} />
    </div>
    <SignUpForm />
  </div>
}