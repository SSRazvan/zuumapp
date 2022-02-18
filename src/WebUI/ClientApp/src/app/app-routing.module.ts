import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { HomeComponent } from './home/home.component';
import { CreateContactComponent } from './modules/contact/components/create-contact/create-contact.component';
import { UpdateContactComponent } from './modules/contact/components/update-contact/update-contact.component';
import { ContactComponent } from './modules/contact/contact.component';
import { CreateFavoriteComponent } from './modules/favorite/components/create-favorite/create-favorite.component';
import { FavoriteComponent } from './modules/favorite/favorite.component';
import { DevEnvGuard } from './nav-menu/dev-env.guard';
import { TodoComponent } from './todo/todo.component';
import { TokenComponent } from './token/token.component';

export const routes: Routes = [

  { path: 'counter', component: CounterComponent },
  { path: 'fetch-data', component: FetchDataComponent },
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'todo', component: TodoComponent, canActivate: [AuthorizeGuard] },
  { path: 'contacts', component: ContactComponent, canActivate: [AuthorizeGuard] },
  { path: 'contacts/create-contact', component: CreateContactComponent, canActivate: [AuthorizeGuard] },
  { path: 'contacts/update-contact/:id', component: UpdateContactComponent, canActivate: [AuthorizeGuard] },
  { path: 'favorites', component: FavoriteComponent, canActivate: [AuthorizeGuard] },
  { path: 'favorites/add-favorite', component: CreateFavoriteComponent, canActivate: [AuthorizeGuard] },
  { path: 'token', component: TokenComponent, canActivate: [AuthorizeGuard, DevEnvGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
