import { Link, useLocation } from "react-router-dom";
import { Button } from './ui/button'
import { cn } from '../lib/utils'

type Props = {
  text: React.ReactNode
  icon: React.ReactNode
  href: string
}

export const MenuLink = ({ text, icon, href }: Props) => {
  const location = useLocation()

  return <Link to={href}>
    <Button variant={'link'}>
      <div className={cn('flex flex-col items-center gap-2', location.pathname === href && 'text-primary')}>
        {icon}
        <p className='text-xs'>{text}</p>
      </div>
    </Button>
  </Link>
}