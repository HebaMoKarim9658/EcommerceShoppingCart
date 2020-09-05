import { Injectable } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardAdminService implements CanActivate {
  constructor(private AccService: AccountService,
    private router: Router) { }
  canActivate(router, state: RouterStateSnapshot) {
    if (this.AccService.isLoggedIn() && this.AccService.IsAdmin())
      return true;
    if (this.AccService.isLoggedIn() && !this.AccService.IsAdmin()) {
      this.router.navigate(['/product'])
      return false;
    }
    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } })
    return false;
  }
}
