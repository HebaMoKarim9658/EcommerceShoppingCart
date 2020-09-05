import { Injectable } from '@angular/core';
import { AccountService } from './account.service';
import { Router, RouterStateSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardUserService {
  constructor(private AccService: AccountService,
    private router: Router) { }
  canActivate(router, state: RouterStateSnapshot) {
    if (this.AccService.isLoggedIn() && !this.AccService.IsAdmin())
      return true;
    if (this.AccService.isLoggedIn() && this.AccService.IsAdmin()) {
      this.router.navigate(['/admin'])
      return false;
    }
    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } })
    return false;
  }
}
