import { H2 } from './typography/h2'
import { Link, useNavigate } from "react-router-dom";
import { Button } from './ui/button'
import { SignUpLink } from '../constants/links/sign-up-link'
import { SignInLink } from '../constants/links/sign-in-link'
import { Logo } from './ui/logo'
import { useContext, useEffect } from 'react'
import { AuthContext } from '../contexts/auth-context'

export const MainPage = () => {
  const { isAuthorized } = useContext(AuthContext)
  const navigate = useNavigate()
  
  useEffect(() => {
    if (isAuthorized) {
      navigate("/users")
    }
  }, [isAuthorized, navigate])

  return <div className='h-screen flex flex-col gap-4 items-center justify-center'>
  <Logo />
  <H2 text={"Онлайн чат"} />
  <div className='mt-2 flex items-center gap-2'>
    <Link to={SignInLink}>
      <Button>Войти в аккаунт</Button>
    </Link>
    <Link to={SignUpLink}>
      <Button variant={"outline"}>
      Зарегистрироваться
      </Button>
    </Link>
  </div>
</div>
}