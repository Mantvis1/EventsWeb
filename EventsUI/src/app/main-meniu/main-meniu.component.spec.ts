import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MainMeniuComponent } from './main-meniu.component';

describe('MainMeniuComponent', () => {
  let component: MainMeniuComponent;
  let fixture: ComponentFixture<MainMeniuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MainMeniuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MainMeniuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
